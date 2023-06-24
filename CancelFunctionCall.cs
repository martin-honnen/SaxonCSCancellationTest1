using Saxon.Api;

namespace SaxonCSCancellationTest1
{
    internal class CancelFunctionCall : ExtensionFunctionCall
    {
        public override XdmValue Call(XdmValue[] arguments, DynamicContext context)
        {
            var cancellationTokenAsXdmItem = arguments[0][0] as XdmExternalObject;

            var cancellationToken = (CancellationToken)cancellationTokenAsXdmItem?.GetObject();

            return new XdmAtomicValue(cancellationToken.IsCancellationRequested);
        }
    }
}