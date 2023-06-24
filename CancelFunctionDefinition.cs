using Saxon.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaxonCSCancellationTest1
{
    internal class CancelFunctionDefinition : ExtensionFunctionDefinition

    {
        public override QName FunctionName => new QName("http://example.com/mf", "cancel");

        public override int MinimumNumberOfArguments => 1;

        public override int MaximumNumberOfArguments => 1;

        public override XdmSequenceType[] ArgumentTypes => new XdmSequenceType[] { new XdmSequenceType(XdmAnyItemType.Instance, ' ') };

        public override ExtensionFunctionCall MakeFunctionCall()
        {
            return new CancelFunctionCall();
        }

        public override XdmSequenceType ResultType(XdmSequenceType[] ArgumentTypes)
        {
            return new XdmSequenceType(XdmAtomicType.BuiltInAtomicType(QName.XS_BOOLEAN), ' ');
        }

        public override bool HasSideEffects => true;

        public override bool TrustResultType => true;
    }
}
