FOR /D %%n IN (HW2t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2ft_*) DO ( 
    IF EXIST %%n/Solution.Tests\bin\Debug\netcoreapp3.1\Solution.Tests.dll (
        clr net-4.7 nunit-console %%n/Solution.Tests\bin\Debug\netcoreapp3.1\Solution.Tests.dll
    )
    ELSE (
        clr net-4.7 nunit-console %%n/Solution.Tests\bin\Debug\netcoreapp3.1\Solution.Tests.dll
    )
)