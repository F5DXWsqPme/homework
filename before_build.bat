FOR /D %%n IN (HW2t_*) DO ( 
    nuget restore %%n/Solution/Solution.csproj
)