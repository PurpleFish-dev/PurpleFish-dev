#ifndef ACCOUNTS_CORE_H
#define ACCOUNTS_CORE_H

#include <QObject>
#include "accountsdata.h"

class CAccounts_core : public QObject
{
    Q_OBJECT
public:
    explicit CAccounts_core(QObject *parent = nullptr);

    std::vector<CTaxcode> get_taxcodes() const { return accounts.get_taxcodes(); }
    CTaxcode get_taxcode(Taxcode_Id id) const { return accounts.get_taxcode(id); }

    bool taxcode_can_add(CTaxcode tc) const { return accounts.taxcode_can_add(tc); }
    bool taxcode_add(CTaxcode tc) { return accounts.taxcode_add(tc); }
    bool taxcode_can_remove(Taxcode_Id id) const { return accounts.taxcode_can_remove(id); }
    bool taxcode_remove(Taxcode_Id id) { return accounts.taxcode_remove(id); }
    bool taxcode_can_replace(CTaxcode tc) const { return accounts.taxcode_can_replace(tc); }
    bool taxcode_replace(CTaxcode tc) { return accounts.taxcode_replace(tc); }


signals:

private:
    AccountsData accounts;
};

#endif // ACCOUNTS_CORE_H
