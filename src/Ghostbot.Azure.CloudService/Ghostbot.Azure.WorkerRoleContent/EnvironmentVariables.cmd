setx GhostbotDiscordToken %GhostbotDiscordToken% /M
setx GhostbotAzureStorageConnectionString %GhostbotAzureStorageConnectionString% /M
setx DestinyApiKey %DestinyApiKey% /M

ECHO GhostbotDiscordToken set to %GhostbotDiscordToken% >> "%TEMP%\StartupLog.txt" 2>&1
ECHO GhostbotAzureStorageConnectionString set to %GhostbotAzureStorageConnectionString% >> "%TEMP%\StartupLog.txt" 2>&1
ECHO DestinyApiKey set to %DestinyApiKey% >> "%TEMP%\StartupLog.txt" 2>&1
EXIT /B 0