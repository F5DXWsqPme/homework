FOR /D %%n IN (HW2t_*) DO ( 
    nuget restore %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2wt_*) DO ( 
    nuget restore %%n/Solution/Solution.sln
)