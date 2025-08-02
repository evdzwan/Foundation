// Foundation's own scripts are loaded on buider.Services.AddFoundation()
builder.Services.AddFoundation();

// Add your own script like this
builder.Services.AddScript<MyComponentScript>();

// Or add all scripts from specific assemblies like this
builder.Services.AddScripts([typeof(MyComponentScript).Assembly, typeof(OtherScript).Assembly]);
