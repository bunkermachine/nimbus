% Function to read an HDF5 file and display its contents
% Datasets that the viewer wishes to view are displayed

function readhdf(filename)

fileinfo = hdf5info(filename);

display('HDF file read!');
display('HDF file info:');
fileinfo

toplevel = fileinfo.GroupHierarchy;

display('Group Hierarchy:');
toplevel

size(toplevel.Groups,2)
display([' datasets available to view.']);
display('Enter dataset which you would like to view:');
idx = 2;

display('Displaying Group ');
idx
toplevel.Groups(idx)

for i = 1 : size(toplevel.Groups(idx).Datasets,2)
    display('Displaying dataset ');
    i
    dset = hdf5read(toplevel.Groups(idx).Datasets(i));
    dset
    display('Displaying data from dataset');
    dset.data
end