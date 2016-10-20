setx GhostbotDiscordToken %GhostbotDiscordToken% /M
setx GhostbotAzureStorageKey %GhostbotAzureStorageKey% /M
setx DestinyApiKey %DestinyApiKey% /M

ECHO GhostbotDiscordToken set to %GhostbotDiscordToken% >> "%TEMP%\StartupLog.txt" 2>&1
ECHO GhostbotAzureStorageKey set to %GhostbotAzureStorageKey% >> "%TEMP%\StartupLog.txt" 2>&1
ECHO DestinyApiKey set to %DestinyApiKey% >> "%TEMP%\StartupLog.txt" 2>&1
EXIT /B 0