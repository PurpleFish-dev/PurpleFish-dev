#include "taxcodes_dlg.h"
#include "ui_taxcodes_dlg.h"

#include "QInputDialog.h"

CTaxcodes_dlg::CTaxcodes_dlg(QWidget *parent, CAccounts_core& core)
    : QDialog(parent)
    , ui(new Ui::CTaxcodes_dlg)
    , core(core)
{
    ui->setupUi(this);
    update();
}

CTaxcodes_dlg::~CTaxcodes_dlg()
{
    delete ui;
}

void CTaxcodes_dlg::update()
{
    ui->comboBox->clear();
    const std::vector<CTaxcode> taxcodes = core.get_taxcodes();
    for(const auto& tc : taxcodes)
    {
        ui->comboBox->addItem(tc.name);
    }
}

void CTaxcodes_dlg::on_btn_Delete_clicked()
{

}

void CTaxcodes_dlg::on_btnAdd_clicked()
{
    bool ok;
    QString text = QInputDialog::getText(this, tr("QInputDialog::getText()"),
                                         tr("User name:"), QLineEdit::Normal,
                                         "", &ok);
    if (ok && !text.isEmpty())
        core.taxcode_add(CTaxcode(text, false));

    update();
}

