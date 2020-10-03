FOR /D %%n IN (HW3t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)