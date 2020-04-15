FOR /D %%n IN (HW2t_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2wt_*) DO ( 
    dotnet test %%n/Solution/Solution.sln
)