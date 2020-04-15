FOR /D %%n IN (HW2t_*) DO ( 
    mstest %%n/Solution/Solution.sln
)

FOR /D %%n IN (HW2wt_*) DO ( 
    mstest %%n/Solution/Solution.sln
)