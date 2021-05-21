WORKSPACE=$(cd `dirname $0`; pwd)
SOURCETABLEDIR=$WORKSPACE/../../Tables/Sources/
TABLERESDIR=$WORKSPACE/../../UnityProject/LegendOfPirate/Assets/StreamingAssets/config

cd $WORKSPACE
printf $WORKSPACE
python ./xlsxconvert/convertxlsx.py -i $SOURCETABLEDIR -o $TABLERESDIR
