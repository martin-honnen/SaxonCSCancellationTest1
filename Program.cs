using Saxon.Api;
using SaxonCSCancellationTest1;
using System.Diagnostics;

var processor = new Processor(true);

processor.RegisterExtensionFunction(new CancelFunctionDefinition());

var docBuilder = processor.NewDocumentBuilder();

var xsltCompiler = processor.NewXsltCompiler();

var xsltExecutable = xsltCompiler.Compile(new Uri(Path.Combine(Environment.CurrentDirectory, "cancel-test1.xsl")));

//Create an Instance of CancellationTokenSource
var CTS = new CancellationTokenSource();
//Set when the token is going to cancel the parallel execution
CTS.CancelAfter(TimeSpan.FromMilliseconds(100));
//Create an instance of ParallelOptions class
var parallelOptions = new ParallelOptions()
{
    MaxDegreeOfParallelism = 4,
    //Set the CancellationToken value
    CancellationToken = CTS.Token
};

try
{
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    var parallelLoopResult = Parallel.ForEach(Enumerable.Range(1, 10000), parallelOptions, (int index, ParallelLoopState loopState) =>
    {
        var xslt30Transformer = xsltExecutable.Load30();

        xslt30Transformer.SetStylesheetParameters(new Dictionary<QName, XdmValue> { { new QName("cancellation-token"), new XdmExternalObject(parallelOptions.CancellationToken) } });

        var inputDoc = docBuilder.Build(new StringReader($"<root><test>{index}</test></root>"));

        using var resultStream = File.OpenWrite($"result-{index}.xml");

        try
        {
            xslt30Transformer.ApplyTemplates(inputDoc, processor.NewSerializer(resultStream));
        }
        catch (SaxonApiException e)
        {
            Console.WriteLine($"ApplyTemplates threw for {index}: {e.Message}");
        }
    });

    stopwatch.Stop();

    Console.WriteLine($"Time taken to execute the transformations : {stopwatch.ElapsedMilliseconds / 1000.0} Seconds");

    Console.WriteLine($"Loop terminated: {parallelLoopResult.IsCompleted}; loop broken after {parallelLoopResult.LowestBreakIteration}");
}
//When the token cancelled, it will throw an exception
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    //Finally dispose the CancellationTokenSource and set its value to null
    CTS.Dispose();
    CTS = null;
}
