// create a class list by the empty ctor or like this
var list = new ClassList("my-class");

// add classes like this; the last parameter is true by default
list.Add("disabled", IsDisabled);

// or use the additional operators
list += "active";

// class list is commonly used like this
public string CssClass => new ClassList().Add("my-class")
                                         .Add("active", IsActive)
                                         .Add("disabled", IsDisabled);

// the resulting class for a non-active, disabled state would be 'my-class disabled'
