#!/bin/sh

readonly WORK_DIR=$(cd $(dirname $0); pwd)
cd $WORK_DIR
openupm add io.github.idreamsofgame.unisharper.core
openupm add com.github.exceldatareader.exceldatareader