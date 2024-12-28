#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include "accounts_core.h"
#include <QMainWindow>

QT_BEGIN_NAMESPACE
namespace Ui {
class MainWindow;
}
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_actionNew_triggered();

    void on_actionOpen_triggered();

    void on_actionSave_triggered();

    void on_actionSave_As_triggered();

    void on_actionTaxcodes_triggered();

private:
    Ui::MainWindow *ui;
    CAccounts_core core;
};
#endif // MAINWINDOW_H
