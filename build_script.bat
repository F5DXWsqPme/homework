FOR /D %%n IN (HW2t_*) DO ( 
    dotnet build %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2wt_*) DO ( 
    msbuild %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2ft_*) DO ( 
    msbuild %%n/Solution/Solution.sln)
