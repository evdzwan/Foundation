// create a style list by the empty ctor or like this
var list = new StyleList(("background-color", "red"));

// add styles like this; the last parameter is true by default
list.Add("opacity", 0, IsHidden);

// or use the additional operators
list += ("pointer-events", "none")

// style list is commonly used like this
public string CssStyle => new StyleList().Add("background-color", "red")
                                         .Add("opacity", 0, IsHidden)
                                         .Add("pointer-events", "none");

// the resulting style for a non-hidden state would be 'background-color:red;pointer-events:none'
