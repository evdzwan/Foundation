using System.Linq.Expressions;
using System.Reflection;

namespace Foundation.Expressions;

public static class ExpressionExtensions
{
    public static object GetContext(this Expression @this)
        => @this.GetContextOrDefault() ?? throw new ArgumentException($"Expression type {@this.Type} is not supported", nameof(@this));

    public static object? GetContextOrDefault(this Expression @this) => @this switch
    {
        ConstantExpression constantExpression => constantExpression.Value,
        LambdaExpression lambdaExpression => lambdaExpression.Body.GetContextOrDefault(),
        MemberExpression { Expression: not null } memberExpression => memberExpression.Expression.GetContextOrDefault(),
        UnaryExpression { NodeType: ExpressionType.Convert } unaryExpression => unaryExpression.Operand.GetContextOrDefault(),
        _ => null
    };

    public static PropertyInfo GetProperty(this Expression @this)
        => @this.GetPropertyOrDefault() ?? throw new ArgumentException($"Expression {@this} does not resolve to a property", nameof(@this));

    public static PropertyInfo? GetPropertyOrDefault(this Expression @this) => @this switch
    {
        LambdaExpression lambdaExpression => lambdaExpression.Body.GetPropertyOrDefault(),
        MemberExpression { Member: PropertyInfo property } => property,
        UnaryExpression { NodeType: ExpressionType.Convert } unaryExpression => unaryExpression.Operand.GetPropertyOrDefault(),
        _ => null
    };

    public static Expression<Func<TResult>> ReplaceParameter<TParameter, TResult>(this Expression<Func<TParameter, TResult>> @this, TParameter parameter)
    {
        var replacer = new ParameterReplacer(@this.Parameters[0], Expression.Constant(parameter));
        var convertedBody = replacer.Visit(@this.Body);

        return Expression.Lambda<Func<TResult>>(convertedBody);
    }

    sealed class ParameterReplacer(ParameterExpression parameter, Expression replacement) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
            => node == parameter ? replacement : base.VisitParameter(node);
    }
}
