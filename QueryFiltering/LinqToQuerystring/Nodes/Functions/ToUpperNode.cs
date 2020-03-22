using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Functions
{
    internal class ToUpperNode : FunctionNode
    {
        public ToUpperNode(BaseNode[] parameters) : base(TypeCashe.String.ToUpper, parameters)
        {
        }
    }
}
