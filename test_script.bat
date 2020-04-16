FOR /D %%n IN (HW2t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2ft_*) DO ( 
    nunit-console %%n/Solution/Solution.sln
)