FOR /D %%n IN (HW2t_*) DO ( 
    dotnet build %%n/Solution/Solution.csproj
)

FOR /D %%n IN (HW2wt_*) DO ( 
    dotnet build %%n/Solution/Solution.csproj
)