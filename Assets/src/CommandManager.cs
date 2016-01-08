using System.Collections;

public class CommandManager
{

    private static CommandManager instance;

    public CommandManager() { }

    public static CommandManager inst
    {
        get
        {
            if (instance == null)
            {
                instance = new CommandManager();
            }
            return instance;
        }
    }

    void Update()
    {

    }

    public void addCommand(System.Type MyClassName)
    {
        ICommand command = System.Activator.CreateInstance(MyClassName) as ICommand;
        command.execute();
    }

}
