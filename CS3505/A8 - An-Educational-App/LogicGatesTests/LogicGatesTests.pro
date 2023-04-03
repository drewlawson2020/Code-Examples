QT += testlib
QT -= gui

CONFIG += qt console warn_on depend_includepath testcase
CONFIG -= app_bundle

TEMPLATE = app

SOURCES +=  tst_logicgatestestss.cpp \
    ../LogicGatesUi/logicgatesmodel.cpp


HEADERS += \
    ../LogicGatesUi/logicgatesmodel.h


INCLUDEPATH += ../LogicGatesUi

DEPENDPATH += ../LogicGatesUi

