namespace MyProject.Shared.DataTransferObjects
{
    public record ResultDataTransferObjects(bool SuccessLoggin, string? ErrorInformation) {
        public static ResultDataTransferObjects Success() => new(true, null);
        public static ResultDataTransferObjects Flase(string ? errorInformation) => new(false, errorInformation);

    }
}
