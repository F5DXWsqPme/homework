FOR /D %%n IN (HW2t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2ft_*) DO ( 
    nunit-console %%n/Solution.Tests\bin\Debug\netcoreapp3.1\Solution.Tests.dll
    nunit-console %%n/Solution.Tests\bin\Release\netcoreapp3.1\Solution.Tests.dll
)