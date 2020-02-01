namespace Task1
{
    //TODO: Не очевидный нейминг. Ты говоришь, что это доступный ресурс, а у него есть статус доступность. Либо там return true, либо meh.
    public interface IAvailableSource
    {
        bool IsAvailable();
        //TODO: это не коннекншен стринга, а какой-то путь к файлу. Даже если у тебя файл в нетворке - это все равно будет путь
        string ConnectionString { get; }
    }
}