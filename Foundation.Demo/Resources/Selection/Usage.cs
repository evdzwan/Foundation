// create a selection like this
readonly ISelection<string> SingleSelection = Selection.Single<string>();
readonly ISelection<string> MultipleSelection = Selection.Multiple<string>();

// or use one of the other overloads
var singleWithDefault = Selection.Single("initial-selection");
var multipleWithDefault = Selection.Multiple(new[] { "initial", "selection" });

// selection is an ilist, so you can use normal list operations
var secondSelectedItem = MultipleSelection[1];

// get the last selected or active item with the cursor property
var activeItem = MultipleSelection.Cursor;

// activate, deactivate or toggle an item
MultipleSelection.Activate("my-value"); // selection now contains "my-value"
MultipleSelection.Deactivate("my-value"); // selection doesn't contain "my-value"
MultipleSelection.Toggle("my-value"); // selection now contains "my-value" again
MultipleSelection.Toggle("my-value"); // selection doesn't contain "my-value"
