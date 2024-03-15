namespace MyProject.Shared.DataTransferObjects
{
    public record ResultWithDataTransferObjects<TData>(bool SuccessLoggin,TData Data, string? ErrorInformation)
    {
        public static ResultWithDataTransferObjects<TData> Success(TData data) => new(true, data,  null);
        public static ResultWithDataTransferObjects<TData> Flase(string? errorInformation) => new(false,default, errorInformation);

    }
}
