using Assignment9;

MenuService ms = new(
    new DbSimulator(
        ConnectionSettings.pricesFile, 
        ConnectionSettings.menuFile,
        ConnectionSettings.exchangeRatesFile,
        ConnectionSettings.needsFile
        )
    );
Presentation presentation = new(ms);
presentation.Run();
