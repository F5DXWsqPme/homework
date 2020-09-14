FOR /D %%n IN (HW3t_*) DO ( 
    dotnet build %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW3wt_*) DO ( 
    msbuild %%n/Solution/Solution.sln
)