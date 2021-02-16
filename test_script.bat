FOR /D %%n IN (HW4t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)