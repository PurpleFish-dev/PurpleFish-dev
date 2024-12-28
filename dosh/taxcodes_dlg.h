#ifndef TAXCODES_DLG_H
#define TAXCODES_DLG_H

#include "accounts_core.h"
#include <QDialog>

namespace Ui {
class CTaxcodes_dlg;
}

class CTaxcodes_dlg : public QDialog
{
    Q_OBJECT

public:
    explicit CTaxcodes_dlg(QWidget *parent, CAccounts_core& core);
    ~CTaxcodes_dlg();

private slots:
    void on_btn_Delete_clicked();
    void on_btnAdd_clicked();

private:
    void update();
    Ui::CTaxcodes_dlg *ui;
    CAccounts_core& core;
};

#endif // TAXCODES_DLG_H
