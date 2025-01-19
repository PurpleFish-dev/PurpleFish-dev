#ifndef TAXCODES_DLG_H
#define TAXCODES_DLG_H

#include "accounts_core.h"
#include <QDialog>
#include <qabstractitemmodel.h>

class ListModel : public QAbstractItemModel
{
    Q_OBJECT
public:
    enum AnimalRoles {
        NameRole = Qt::UserRole + 1,
        ObsoleteRole
    };

    ListModel(CAccounts_core& core) : core(core) {}
    virtual int rowCount(const QModelIndex &parent = QModelIndex()) const override { return m_animals.count(); }
    virtual int columnCount(const QModelIndex &parent = QModelIndex()) const override { return 1; }
    virtual QModelIndex index(int row, int column, const QModelIndex &parent = QModelIndex()) const override { return createIndex(row, column, nullptr); }
    QVariant data(const QModelIndex & index, int role) const override {
        if (index.row() < 0 || index.row() >= m_animals.count())
            return QVariant();

        const CTaxcode &animal = m_animals[index.row()];
        if (role == NameRole)
            return animal.name;
        else if (role == ObsoleteRole)
            return animal.obsolete;
        return QVariant();
    }
    QModelIndex parent(const QModelIndex &child) const  override { return QModelIndex(); }
    void addAnimal(const CTaxcode &animal);
private:
   CAccounts_core& core;

    QList<CTaxcode> m_animals;
};

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

    void on_comboBox_currentTextChanged(const QString &arg1);

    void on_comboBox_editTextChanged(const QString &arg1);

    void on_comboBox_currentTextSaved();



    void on_listView_indexesMoved(const QModelIndex &indexes);

private:
    void update();
    Ui::CTaxcodes_dlg *ui;
    CAccounts_core& core;
};

#endif // TAXCODES_DLG_H
