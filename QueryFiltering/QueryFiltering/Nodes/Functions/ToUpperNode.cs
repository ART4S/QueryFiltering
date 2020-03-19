using QueryFiltering.Helpers;
using QueryFiltering.Nodes.Base;

namespace QueryFiltering.Nodes.Functions
{
    internal class ToUpperNode : FunctionNode
    {
        public ToUpperNode(BaseNode[] parameters) : base(TypeCashe.String.ToUpper, parameters)
        {
        }
    }
}
