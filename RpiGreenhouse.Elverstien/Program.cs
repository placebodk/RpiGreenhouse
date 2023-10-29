using RpiGreenhouse.Elverstien;

var loggerFactory = new LoggerFactory();
var elverstien = new Elverstien(loggerFactory);
elverstien.Run();
