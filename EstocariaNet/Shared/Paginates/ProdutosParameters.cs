namespace EstocariaNet.Shared.DTOs
{
    public class ProdutosParameters
    {
        const int maxPageSize = 50;               //tamanho maximo da pagina permitido 50produtos
        public int PageNumber { get; set; } = 1; //numero de pagina recuperada
        private int _pageSize = 10;
        public int PageSize{                       //propriedade para configurar o tamanho da pagina
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }


    }
}