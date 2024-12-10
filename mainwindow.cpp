#include "mainwindow.h"
#include "./ui_mainwindow.h"

#include "accountsdata.h"
#include "cxmlserializer.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    AccountsData data;
    //data.ImportXML("C:\\Users\\Dan\\Documents\\Data\\Accounts\\Dan & Maggie\\Current\\aaa01.xml");


    CXMLSerializer::importXML("C:\\Users\\Dan\\Documents\\Data\\Accounts\\Dan & Maggie\\Current\\aaa01.xml", data);
}

MainWindow::~MainWindow()
{
    delete ui;
}

