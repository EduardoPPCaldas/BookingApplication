namespace Domain.Enums
{
    public enum Action
    {
        Pay = 0,
        Finish = 1, // afther paid and used
        Cancel = 2, // can never be paid
        Refound = 3,
        Reopen = 4,
    }
}