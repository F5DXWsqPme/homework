FOR /D %%n IN (HW4t_*) DO ( 
    dotnet build %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW4wt_*) DO ( 
    msbuild %%n/Solution/Solution.sln
)