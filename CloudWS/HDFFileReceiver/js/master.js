<<<<<<< HEAD
$(function() {  $("#add-button").click(function(e) {    e.preventDefault();    addParameter();  });      // Setup the HDF uploader  $("#upload-hdf").jup({    json: true,    onComplete: displayHDFData  });  // Resize the map whenever the window is resized  $(window).bind('load resize', function() {    $("#map").height($(window).height() - 67);  });    initDataTable();});/* Initialization functions */var dataTable;function initDataTable(){  dataTable = $("#data-table").dataTable({    "aoColumns": [      { "sTitle": "Date" },      { "sTitle": "Surf. Pressure" },      { "sTitle": "Air Temp." },      { "sTitle": "Rel. Hum." },      { "sTitle": "Cloud Cov." },      { "sTitle": "Precipitation" },      { "sTitle": "Down. Short. Rad." },      { "sTitle": "Down. Long. Rad. "}    ],   	"bSortClasses": false  });    $('#data-table tbody td').hover(function() {      var iCol = $('td').index(this) % 5;    	var nTrs = dataTable.fnGetNodes();    	$('td:nth-child('+(iCol+1)+')', nTrs).addClass('highlighted');    },    function() {    	var nTrs = dataTable.fnGetNodes();    	$('td.highlighted', nTrs).removeClass('highlighted');  });}/* Parameter List */function addParameter(){  var newFilter = $("#filter-template").clone();  $("#filters").append(newFilter);  }function hidePanel(){}function displayHDFData(response, formId){  dataTable.fnAddData([    [ 2010021818 , 1014 , 23 , 27 , "NaN" , "NaN" , "NaN" , "NaN" ],    [ 2010021821 , 1015.7 , 22.4 , 30.5 , 23 , 0 , 0 , 336 ],    [ 2010021900 , 1016.7 , 30.2 , 19.5 , 12 , 0 , 130 , 338 ],    [ 2010021903 , 1014.8 , 35.1 , 14 , 10 , 0 , 880 , 368 ],    [ 2010021906 , 1011.5 , 37.2 , 11.4 , 5 , 0 , 945 , 375 ],      [ 2010021909 , 1010 , 35.8 , 11.8 , 1 , 0 , 560 , 381 ],    [ 2010021912 , 1012.1 , 29.8 , 16.7 , 0 , 0 , 292 , 371 ],    [ 2010021915 , 1012.4 , 26.9 , 19.4 , 0 , 0 , 0 , 345 ],    [ 2010021918 , 1011.3 , 25.6 , 23.5 , 4 , 0 , 0 , 343 ]  ]);}
=======
$(function() {

  $("#add-button").click(function(e) {
    e.preventDefault();
    addParameter();
  });
    
  // Setup the HDF uploader
  $("#upload-hdf").jup({
    json: true,
    onComplete: displayHDFData
  });

  // Resize the map whenever the window is resized
  $(window).bind('load resize', function() {
    $("#map").height($(window).height() - 67);
  });
  
  initDataTable();
});

/* Initialization functions */

var dataTable;
function initDataTable()
{
  dataTable = $("#data-table").dataTable({
    "aoColumns": [
      { "sTitle": "Date" },
      { "sTitle": "Wind U10" },
      { "sTitle": "Wind V10" },
      { "sTitle": "Wind FF10" },
      { "sTitle": "Wind DD10" },
      { "sTitle": "Surf. Pressure" },
      { "sTitle": "Air Temp." },
      { "sTitle": "Rel. Hum." },
      { "sTitle": "Cloud Cov." },
      { "sTitle": "Precipitation" },
      { "sTitle": "Down. Short. Rad." },
      { "sTitle": "Down. Long. Rad. "}
    ],
   	"bSortClasses": false
  });
  
  $('#data-table tbody td').hover(function() {
      var iCol = $('td').index(this) % 5;
    	var nTrs = dataTable.fnGetNodes();
    	$('td:nth-child('+(iCol+1)+')', nTrs).addClass('highlighted');
    },
    function() {
    	var nTrs = dataTable.fnGetNodes();
    	$('td.highlighted', nTrs).removeClass('highlighted');
  });
}

/* Parameter List */
function addParameter()
{
  var newFilter = $("#filter-template").clone();
  $("#filters").append(newFilter);
  
}

function displayHDFData(response, formId)
{
  dataTable.fnAddData([
    [ 2010021818 , -2.8 , 0.3 , 2.8 , 95.9 , 1014 , 23 , 27 , "NaN" , "NaN" , "NaN" , "NaN" ],
    [ 2010021821 , -2.7 , -0.8 , 2.8 , 73.5 , 1015.7 , 22.4 , 30.5 , 23 , 0 , 0 , 336 ],
    [ 2010021900 , -2.1 , -2.8 , 3.5 , 37.5 , 1016.7 , 30.2 , 19.5 , 12 , 0 , 130 , 338 ],
    [ 2010021903 , -3 , 0 , 3 , 90.2 , 1014.8 , 35.1 , 14 , 10 , 0 , 880 , 368 ],
    [ 2010021906 , -3.5 , 1.4 , 3.7 , 112.6 , 1011.5 , 37.2 , 11.4 , 5 , 0 , 945 , 375 ],  
    [ 2010021909 , -3.7 , 1.7 , 4.1 , 115.1 , 1010 , 35.8 , 11.8 , 1 , 0 , 560 , 381 ],
    [ 2010021912 , -3.8 , 0.7 , 3.9 , 100 , 1012.1 , 29.8 , 16.7 , 0 , 0 , 292 , 371 ],
    [ 2010021915 , -3.9 , -1.6 , 4.2 , 68.2 , 1012.4 , 26.9 , 19.4 , 0 , 0 , 0 , 345 ],
    [ 2010021918 , -2.3 , -4.2 , 4.8 , 28.1 , 1011.3 , 25.6 , 23.5 , 4 , 0 , 0 , 343 ]
  ]);
}
>>>>>>> d4a516dcb613988c559097705c11de866ced072d
