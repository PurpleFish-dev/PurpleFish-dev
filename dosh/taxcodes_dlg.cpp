#include "taxcodes_dlg.h"
#include "ui_taxcodes_dlg.h"

#include "QInputDialog.h"
#include <QLineEdit>

void ListModel::addAnimal(const CTaxcode &animal)
{
    m_animals.append(animal);//beginInsertRows(QModelIndex(), rowCount(), rowCount());
    //m_animals << animal;
    //endInsertRows();
}

CTaxcodes_dlg::CTaxcodes_dlg(QWidget *parent, CAccounts_core& core)
    : QDialog(parent)
    , ui(new Ui::CTaxcodes_dlg)
    , core(core)
{
    ui->setupUi(this);

    ListModel model = ListModel(core);

    const std::vector<CTaxcode> taxcodes = core.get_taxcodes();
    for(const auto& tc : taxcodes)
    {
        QVariant variant;
        variant.setValue(tc.id);
        model.addAnimal(tc);

        const Taxcode_Id currentId = variant.value<Taxcode_Id>();
    }






    ui->listView->setModel(&model);

// for i in entries:
//     item = QtGui.QStandardItem(i)
//     model.appendRow(item)



//connect(ui->comboBox->lineEdit(), &QLineEdit::editingFinished,
    //        this, [&] () { emit on_comboBox_currentTextSaved(ui->comboBox->currentText()); });

    connect(ui->comboBox->lineEdit(), &QLineEdit::editingFinished, this, &CTaxcodes_dlg::on_comboBox_currentTextSaved);

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
        QVariant variant;
        variant.setValue(tc.id);
        ui->comboBox->addItem(tc.name, variant);

        const Taxcode_Id currentId = variant.value<Taxcode_Id>();
    }

    const int count = ui->comboBox->count();
    const int currentIndex = ui->comboBox->currentIndex();
}

void CTaxcodes_dlg::on_btn_Delete_clicked()
{
    const Taxcode_Id id =  ui->comboBox->currentData().value<Taxcode_Id>();
    core.taxcode_remove(id);
    update();
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


void CTaxcodes_dlg::on_comboBox_currentTextChanged(const QString &arg1)
{
    int tmp =0;
}


void CTaxcodes_dlg::on_comboBox_editTextChanged(const QString &arg1)
{
    const QString ct = ui->comboBox->itemText(ui->comboBox->currentIndex());
    if(ct != arg1)
    {
        int tmp =0;
    }
}

void CTaxcodes_dlg::on_comboBox_currentTextSaved()
{
    const QString arg1  = ui->comboBox->currentText();
    const Taxcode_Id currentId = ui->comboBox->currentData().value<Taxcode_Id>();
    const int idx = ui->comboBox->currentIndex();
    const Taxcode_Id index_id = ui->comboBox->itemData(idx).value<Taxcode_Id>();


    if(!ui->comboBox->currentData().isNull())
    {
        const CTaxcode tc = core.get_taxcode(ui->comboBox->currentData().value<Taxcode_Id>());
        if(tc.name != arg1)
        {
            CTaxcode newtc = CTaxcode(arg1, tc.obsolete, tc.id);
            core.taxcode_replace(newtc);
        }
    }
    update();
}

void CTaxcodes_dlg::on_listView_indexesMoved(const QModelIndex &indexes)
{

}

