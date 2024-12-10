#ifndef CTAXCODE_H
#define CTAXCODE_H

#include <QString>
#include <QUuid>
#include <QDateTime>

//#include <QXmlStreamReader>

typedef QUuid TaxCode_Id;
typedef QUuid Property_Id;
typedef QUuid Payee_Id;
typedef QUuid Category_Id;
typedef QUuid Account_Id;
typedef QUuid Entry_Id;

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

class CTaxCode
{
public:
	CTaxCode(const CTaxCode& arg) : name(arg.name), obsolete(arg.obsolete) { }
	CTaxCode(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }

	const QString name;
	const bool obsolete;
};

class CProperty
{
public:
	CProperty(const CProperty& arg) : name(arg.name), obsolete(arg.obsolete) { }
	CProperty(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }

	const QString name;
	const bool obsolete;
};

class CPayee
{
public:
	CPayee(const CPayee& arg) : name(arg.name), obsolete(arg.obsolete) { }
	CPayee(QString name, bool obsolete) : name(name.trimmed()), obsolete(obsolete) { }

	const QString name;
	const bool obsolete;
};

class CCategory
{
public:
	CCategory(const CCategory& arg)
		: name(arg.name), obsolete(arg.obsolete), income(arg.income), taxcode_id(arg.taxcode_id), property_specific(arg.property_specific) { }

    CCategory(QString name, bool obsolete, bool income, TaxCode_Id taxCode_id, bool property_specific)
		: name(name.trimmed()), obsolete(obsolete), income(income), taxcode_id(taxcode_id), property_specific(property_specific) { }

	const QString name;
	const bool obsolete;
	const bool income;
	const TaxCode_Id taxcode_id;
	const bool property_specific;
};

class CAccount
{
public:
	CAccount(const CAccount& arg)
        : name(arg.name), external(arg.external), type(arg.type), hidden(arg.hidden), lock(arg.lock), locked_until(arg.locked_until), reconciled_on(arg.reconciled_on) { }

    CAccount(QString name, bool external, eAccType::AccType type, bool hidden, eAccLock::AccLock lock, QDateTime locked_until, QDateTime reconciled_on)
        : name(name.trimmed()), external(external), type(type), hidden(hidden), lock(lock), locked_until(locked_until), reconciled_on(reconciled_on) { }

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
        , account_id(arg.account_id)
        , transfer_id(arg.transfer_id)
        , amount(arg.amount) { }

    CEntry(Entry_Id id
    //, eEntryType::EntryType type
    , QString description
    , QString importDescription
    , QDateTime date
    , QUuid reciept_id
    , Payee_Id payee_id
    , Category_Id category_id
    , Property_Id property_id
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
        , account_id(account_id)
        , transfer_id(transfer_id)
        , amount(amount) { }

	const Entry_Id id;
    //eEntryType::EntryType type;
	const QString description;
	const QString importDescription;
	const QDateTime date;
	const QUuid reciept_id;
	const Payee_Id payee_id;
	const Category_Id category_id;
	const Property_Id property_id;
	const Account_Id account_id;
	const Account_Id transfer_id;
	const double amount;//todo QDecimal
};

#endif
