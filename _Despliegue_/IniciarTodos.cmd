@echo off

for /D %%d in (*) do (
    cd %%d
    start Kaomi.WebAPI.exe %%d
)
