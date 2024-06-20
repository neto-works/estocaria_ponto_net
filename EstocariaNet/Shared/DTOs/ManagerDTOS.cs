namespace EstocariaNet.Shared.DTOs
{
    public abstract class ManagerDTOS<T,C,U>
    {
        protected abstract T ConvertCreateDtoToClass(C crateDto);
        protected abstract void UpdateClassToDto(T antigo, U tipoDto);
    }
}