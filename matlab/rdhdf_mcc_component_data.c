/*
 * MATLAB Compiler: 4.8 (R2008a)
 * Date: Thu May 06 09:15:15 2010
 * Arguments: "-B" "macro_default" "-m" "-W" "main" "-T" "link:exe" "RdHDF.m"
 * "-o" "RDHDF" 
 */

#include "mclmcrrt.h"

#ifdef __cplusplus
extern "C" {
#endif
const unsigned char __MCC_RDHDF_session_key[] = {
    'A', 'D', 'A', '2', '4', '3', '4', 'B', '4', '8', '5', 'A', '9', '9', 'F',
    '8', '9', '9', '0', '4', '3', '0', 'E', '6', '4', '8', '2', '7', '8', 'C',
    '9', '9', '3', 'B', '3', '6', 'B', 'C', '9', 'D', 'F', 'E', '9', '1', 'F',
    '5', 'F', '5', '7', 'A', '5', 'A', '7', '2', '2', '8', '7', '9', 'E', 'E',
    'D', '2', '5', 'E', '4', '5', '9', '7', '8', 'D', '8', 'A', '9', '3', '6',
    '8', 'A', '5', 'B', 'D', 'D', '1', '5', '1', '9', '3', '7', '0', '3', '7',
    '4', '0', '3', 'B', '6', '8', '0', '8', '5', '2', 'A', 'A', '8', '2', '4',
    '2', '1', 'B', 'A', 'F', '2', 'F', '8', 'E', 'F', '7', 'B', '0', '4', 'B',
    '6', '3', '3', '5', 'A', '3', 'C', 'A', 'B', 'D', '8', '7', '3', 'C', 'F',
    'D', 'B', 'E', '5', '3', '5', '6', '9', 'F', 'B', 'F', 'B', '0', 'E', '1',
    '7', 'B', '9', '0', '0', '4', '7', '1', '1', '8', 'D', '5', '2', 'D', '8',
    '8', 'F', '1', 'E', '3', '8', 'E', '8', 'A', 'E', '6', 'D', '1', 'C', '7',
    '7', '4', 'D', '8', '9', '8', '1', 'E', '3', '5', '3', '2', '3', '8', '6',
    'E', 'C', 'A', 'C', 'A', '3', 'C', 'A', '7', '5', 'A', '1', 'C', '3', 'B',
    'B', '2', 'D', 'F', 'E', '1', '8', '4', '9', '5', '4', '8', 'B', 'C', '1',
    '7', 'C', 'E', '5', '7', '1', '4', '1', '5', '8', '3', '3', '5', 'F', '8',
    '0', 'E', '4', 'B', 'B', '4', 'F', 'F', '9', '0', '3', '5', '9', '3', '4',
    '0', '\0'};

const unsigned char __MCC_RDHDF_public_key[] = {
    '3', '0', '8', '1', '9', 'D', '3', '0', '0', 'D', '0', '6', '0', '9', '2',
    'A', '8', '6', '4', '8', '8', '6', 'F', '7', '0', 'D', '0', '1', '0', '1',
    '0', '1', '0', '5', '0', '0', '0', '3', '8', '1', '8', 'B', '0', '0', '3',
    '0', '8', '1', '8', '7', '0', '2', '8', '1', '8', '1', '0', '0', 'C', '4',
    '9', 'C', 'A', 'C', '3', '4', 'E', 'D', '1', '3', 'A', '5', '2', '0', '6',
    '5', '8', 'F', '6', 'F', '8', 'E', '0', '1', '3', '8', 'C', '4', '3', '1',
    '5', 'B', '4', '3', '1', '5', '2', '7', '7', 'E', 'D', '3', 'F', '7', 'D',
    'A', 'E', '5', '3', '0', '9', '9', 'D', 'B', '0', '8', 'E', 'E', '5', '8',
    '9', 'F', '8', '0', '4', 'D', '4', 'B', '9', '8', '1', '3', '2', '6', 'A',
    '5', '2', 'C', 'C', 'E', '4', '3', '8', '2', 'E', '9', 'F', '2', 'B', '4',
    'D', '0', '8', '5', 'E', 'B', '9', '5', '0', 'C', '7', 'A', 'B', '1', '2',
    'E', 'D', 'E', '2', 'D', '4', '1', '2', '9', '7', '8', '2', '0', 'E', '6',
    '3', '7', '7', 'A', '5', 'F', 'E', 'B', '5', '6', '8', '9', 'D', '4', 'E',
    '6', '0', '3', '2', 'F', '6', '0', 'C', '4', '3', '0', '7', '4', 'A', '0',
    '4', 'C', '2', '6', 'A', 'B', '7', '2', 'F', '5', '4', 'B', '5', '1', 'B',
    'B', '4', '6', '0', '5', '7', '8', '7', '8', '5', 'B', '1', '9', '9', '0',
    '1', '4', '3', '1', '4', 'A', '6', '5', 'F', '0', '9', '0', 'B', '6', '1',
    'F', 'C', '2', '0', '1', '6', '9', '4', '5', '3', 'B', '5', '8', 'F', 'C',
    '8', 'B', 'A', '4', '3', 'E', '6', '7', '7', '6', 'E', 'B', '7', 'E', 'C',
    'D', '3', '1', '7', '8', 'B', '5', '6', 'A', 'B', '0', 'F', 'A', '0', '6',
    'D', 'D', '6', '4', '9', '6', '7', 'C', 'B', '1', '4', '9', 'E', '5', '0',
    '2', '0', '1', '1', '1', '\0'};

static const char * MCC_RDHDF_matlabpath_data[] = 
  { "RDHDF/", "toolbox/compiler/deploy/",
    "$TOOLBOXMATLABDIR/general/", "$TOOLBOXMATLABDIR/ops/",
    "$TOOLBOXMATLABDIR/lang/", "$TOOLBOXMATLABDIR/elmat/",
    "$TOOLBOXMATLABDIR/elfun/", "$TOOLBOXMATLABDIR/specfun/",
    "$TOOLBOXMATLABDIR/matfun/", "$TOOLBOXMATLABDIR/datafun/",
    "$TOOLBOXMATLABDIR/polyfun/", "$TOOLBOXMATLABDIR/funfun/",
    "$TOOLBOXMATLABDIR/sparfun/", "$TOOLBOXMATLABDIR/scribe/",
    "$TOOLBOXMATLABDIR/graph2d/", "$TOOLBOXMATLABDIR/graph3d/",
    "$TOOLBOXMATLABDIR/specgraph/", "$TOOLBOXMATLABDIR/graphics/",
    "$TOOLBOXMATLABDIR/uitools/", "$TOOLBOXMATLABDIR/strfun/",
    "$TOOLBOXMATLABDIR/imagesci/", "$TOOLBOXMATLABDIR/iofun/",
    "$TOOLBOXMATLABDIR/audiovideo/", "$TOOLBOXMATLABDIR/timefun/",
    "$TOOLBOXMATLABDIR/datatypes/", "$TOOLBOXMATLABDIR/verctrl/",
    "$TOOLBOXMATLABDIR/codetools/", "$TOOLBOXMATLABDIR/helptools/",
    "$TOOLBOXMATLABDIR/winfun/", "$TOOLBOXMATLABDIR/demos/",
    "$TOOLBOXMATLABDIR/timeseries/", "$TOOLBOXMATLABDIR/hds/",
    "$TOOLBOXMATLABDIR/guide/", "$TOOLBOXMATLABDIR/plottools/",
    "toolbox/local/", "toolbox/shared/dastudio/",
    "$TOOLBOXMATLABDIR/datamanager/", "toolbox/compiler/" };

static const char * MCC_RDHDF_classpath_data[] = 
  { "" };

static const char * MCC_RDHDF_libpath_data[] = 
  { "" };

static const char * MCC_RDHDF_app_opts_data[] = 
  { "" };

static const char * MCC_RDHDF_run_opts_data[] = 
  { "" };

static const char * MCC_RDHDF_warning_state_data[] = 
  { "off:MATLAB:dispatcher:nameConflict" };


mclComponentData __MCC_RDHDF_component_data = { 

  /* Public key data */
  __MCC_RDHDF_public_key,

  /* Component name */
  "RDHDF",

  /* Component Root */
  "",

  /* Application key data */
  __MCC_RDHDF_session_key,

  /* Component's MATLAB Path */
  MCC_RDHDF_matlabpath_data,

  /* Number of directories in the MATLAB Path */
  38,

  /* Component's Java class path */
  MCC_RDHDF_classpath_data,
  /* Number of directories in the Java class path */
  0,

  /* Component's load library path (for extra shared libraries) */
  MCC_RDHDF_libpath_data,
  /* Number of directories in the load library path */
  0,

  /* MCR instance-specific runtime options */
  MCC_RDHDF_app_opts_data,
  /* Number of MCR instance-specific runtime options */
  0,

  /* MCR global runtime options */
  MCC_RDHDF_run_opts_data,
  /* Number of MCR global runtime options */
  0,
  
  /* Component preferences directory */
  "RDHDF_EAA5D47C2EC33250BAD52E9D71C86C91",

  /* MCR warning status data */
  MCC_RDHDF_warning_state_data,
  /* Number of MCR warning status modifiers */
  1,

  /* Path to component - evaluated at runtime */
  NULL

};

#ifdef __cplusplus
}
#endif


