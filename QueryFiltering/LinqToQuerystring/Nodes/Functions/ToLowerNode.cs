using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Functions
{
    internal class ToLowerNode : FunctionNode
    {
        public ToLowerNode(BaseNode[] parameters) : base(TypeCashe.String.ToLower, parameters)
        {
        }
    }
}
