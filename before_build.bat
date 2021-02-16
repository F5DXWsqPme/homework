FOR /D %%n IN (HW4t_*) DO ( 
    nuget restore %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW4wt_*) DO ( 
    nuget restore %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW4ft_*) DO ( 
    nuget restore %%n/Solution/Solution.sln
)