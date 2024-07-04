namespace MinimalAPIERP.Dtos
{
    public class ProductoDTO
    {
        public string? Nombre { get; set; }
        public CategoriaDTO? IdCategoria { get; set; }
    }

    public class CategoriaDTO
    {
        public string? Descripcion { get; set; }
    }
}
