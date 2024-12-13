#ifndef CTAXCODE_H
#define CTAXCODE_H

#include <QString>
#include <QUuid>
#include <QDateTime>

template <class Tag>
class Id
{
public:
    Id() : value_() { }

    bool operator<(const Id& rhs) const { return value_ < rhs.value_; }

    bool isNull() const { return value_.isNull(); }

    bool operator==(const Id& rhs) const { return value_ == rhs.value_; }

    static Id fromString(QAnyStringView string) noexcept { return Id(QUuid::fromString(string)); }

private:
    Id(const QUuid& value) : value_(value) { }
    QUuid value_;
};

struct Taxcode_Id_tag {};
using Taxcode_Id = Id<Taxcode_Id_tag>;

struct Property_Id_tag {};
using Property_Id = Id<Property_Id_tag>;

struct Payee_Id_tag {};
using Payee_Id = Id<Payee_Id_tag>;

struct Category_Id_tag {};
using Category_Id = Id<Category_Id_tag>;

struct Account_Id_tag {};
using Account_Id = Id<Account_Id_tag>;

struct Entry_Id_tag {};
using Entry_Id = Id<Entry_Id_tag>;

class eAccType : public QObject
{
    Q_OBJECT
public:
    enum class AccType {
        Credit,
        Liability
    }; Q_ENUM(AccType)
};

class eAccLock : public QObject
{
    Q_OBJECT
public:
    enum class AccLock {
        Locked,
        LockedUntil,
        Open
    }; Q_ENUM(AccLock)
};

class CTaxcode
{
public:
    CTaxcode(const CTaxcode& arg) : name(arg.name), obsolete(arg.obsolete) { }
    CTaxcode(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }
    bool operator==(const CTaxcode& rhs) const { return (name == rhs.name) && (obsolete == rhs.obsolete); }

	const QString name;
	const bool obsolete;
};

class CProperty
{
public:
	CProperty(const CProperty& arg) : name(arg.name), obsolete(arg.obsolete) { }
	CProperty(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }
    bool operator==(const CProperty& rhs) const { return (name == rhs.name) && (obsolete == rhs.obsolete); }

	const QString name;
	const bool obsolete;
};

class CPayee
{
public:
	CPayee(const CPayee& arg) : name(arg.name), obsolete(arg.obsolete) { }
	CPayee(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }
    bool operator==(const CPayee& rhs) const { return (name == rhs.name) && (obsolete == rhs.obsolete); }

	const QString name;
	const bool obsolete;
};

class CCategory
{
public:
	CCategory(const CCategory& arg)
        : name(arg.name), obsolete(arg.obsolete), income(arg.income), taxcode_id(arg.taxcode_id), taxcode_specific(arg.taxcode_specific) { }

    CCategory(QString name, bool obsolete, bool income, Taxcode_Id taxcode_id, bool taxcode_specific)
        : name(name.trimmed()), obsolete(obsolete), income(income), taxcode_id(taxcode_id), taxcode_specific(taxcode_specific) { }

    bool operator==(const CCategory& rhs) const { return (name == rhs.name) && (obsolete == rhs.obsolete)&& (income == rhs.income)&& (taxcode_id == rhs.taxcode_id)&& (taxcode_specific == rhs.taxcode_specific); }

	const QString name;
	const bool obsolete;
	const bool income;
    const Taxcode_Id taxcode_id;
    const bool taxcode_specific;
};

class CAccount
{
public:
	CAccount(const CAccount& arg)
        : name(arg.name), external(arg.external), type(arg.type), hidden(arg.hidden), lock(arg.lock), locked_until(arg.locked_until), reconciled_on(arg.reconciled_on) { }

    CAccount(QString name, bool external, eAccType::AccType type, bool hidden, eAccLock::AccLock lock, QDateTime locked_until, QDateTime reconciled_on)
        : name(name.trimmed()), external(external), type(type), hidden(hidden), lock(lock), locked_until(locked_until), reconciled_on(reconciled_on) { }

    bool operator==(const CAccount& rhs) const
    {
        return (name == rhs.name)
        && (external == rhs.external)
        && (type == rhs.type)
        && (hidden == rhs.hidden)
        && (lock == rhs.lock)
        && (locked_until == rhs.locked_until)
        && (reconciled_on == rhs.reconciled_on);
    }

    const QString name;
    const bool external;
    const eAccType::AccType type;
    const bool hidden;
    const eAccLock::AccLock lock;
    const QDateTime locked_until;
    const QDateTime reconciled_on;
};

class CEntry
{
public:
	CEntry(const CEntry& arg)
        : description(arg.description)
        , importDescription(arg.description)
        , date(arg.date)
        , reciept_id(arg.reciept_id)
        , payee_id(arg.payee_id)
        , category_id(arg.category_id)
        , property_id(arg.property_id)
        , taxcode_id(arg.taxcode_id)
        , account_id(arg.account_id)
        , transfer_id(arg.transfer_id)
        , amount(arg.amount) { }

    CEntry(
    //, eEntryType::EntryType type
    QString description
    , QString importDescription
    , QDateTime date
    , QUuid reciept_id
    , Payee_Id payee_id
    , Category_Id category_id
    , Property_Id property_id
    , Taxcode_Id taxcode_id
    , Account_Id account_id
    , Account_Id transfer_id
    , double amount)//todo QDecimal
        : description(description)
        , importDescription(description)
        , date(date)
        , reciept_id(reciept_id)
        , payee_id(payee_id)
        , category_id(category_id)
        , property_id(property_id)
        , taxcode_id(taxcode_id)
        , account_id(account_id)
        , transfer_id(transfer_id)
        , amount(amount) { }

    //eEntryType::EntryType type;
	const QString description;
	const QString importDescription;
	const QDateTime date;
	const QUuid reciept_id;
	const Payee_Id payee_id;
	const Category_Id category_id;
    const Property_Id property_id;
    const Taxcode_Id taxcode_id;
	const Account_Id account_id;
	const Account_Id transfer_id;
	const double amount;//todo QDecimal
};

#endif
