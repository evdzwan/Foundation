// create an asyncvalue like this; even though the actual value hasn't been retrieved yet, the type can be assigned to a readonly variable
readonly IAsyncValue<string> Value = AsyncValue.Create(async () =>
{
    // retrieve your data asynchronously
    return await MyRepository.Get(MyId);
});

// retrieve actual value like this
var str = await Value.GetValue();

// value is cached, so this call won't do anything and just return the value
var str2 = await Value.GetValue();

// you can reset the cache so the delegate will be called again
Value.Reset();
var str3 = await Value.GetValue();
