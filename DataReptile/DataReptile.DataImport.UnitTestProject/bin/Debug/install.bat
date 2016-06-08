%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe E:\Projects\PreProjects\Source\ChinaCCW\DataReptile\DataReptile.WindowsService\bin\Debug\DataReptile.WindowsService.exe
Net Start TimerExecuteService
sc config TimerExecuteService start= auto
pause