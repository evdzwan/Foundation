// create an asynccollection like this; even though the actual collection hasn't been retrieved yet, the type can be assigned to a readonly variable
readonly IAsyncCollection<string> Values = AsyncCollection.Create(async page =>
{
    // retrieve your data asynchronously, using the supplied page parameter
    return await MyRespository.GetCollection(page.Skip, page.Take);
}, windowSize: 50);

// retrieve data like this (returns max 10 items)
var view = await Value.GetView(new(Skip: 0, Take: 10));

// view is cached, so this call won't do anything and just return the view
var view2 = await Value.GetView(new(Skip: 0, Take: 10));

// this call won't retrieve data, because of the window size
var view3 = await Value.GetView(new(Skip: 10, Take: 40));

// this one does
var view4 = await Value.GetView(new(Skip: 50, Take: 10));

// you can reset the cache so the delegate will be called again
Values.Reset();
var view5 = await Value.GetView(new(Skip: 10, Take: 40));
