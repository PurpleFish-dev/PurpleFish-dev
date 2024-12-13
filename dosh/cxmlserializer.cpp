#include "cxmlserializer.h"

#include <QFile>
#include <QMetaEnum>

bool CXMLSerializer::readTaxCode(QXmlStreamReader& reader, AccountsData& accs)
{
    Taxcode_Id id;
    QString name;
    bool obsolete = false;

    while(reader.readNextStartElement()){
            if(reader.name().toString() == "Id")
                id = Taxcode_Id::fromString(reader.readElementText());
            else if(reader.name().toString() == "Name")
                name = reader.readElementText();
            else if(reader.name().toString() == "Obsolete")
                obsolete = reader.readElementText() == "true" ? true : false;
            else
                reader.skipCurrentElement();
    }

    return accs.taxcode_add(CTaxcode(name, obsolete), id);
}

bool CXMLSerializer::readProperties(QXmlStreamReader& reader, AccountsData& accs)
{
    Property_Id id;
    QString name;
    bool obsolete = false;

    while(reader.readNextStartElement()){
            if(reader.name().toString() == "Id")
                id = Property_Id::fromString(reader.readElementText());
            else if(reader.name().toString() == "Name")
                name = reader.readElementText();
            else if(reader.name().toString() == "Obsolete")
                obsolete = reader.readElementText() == "true" ? true : false;
            else
                reader.skipCurrentElement();
    }

    return accs.property_add(CProperty(name, obsolete), id);
}

bool CXMLSerializer::readPayees(QXmlStreamReader& reader, AccountsData& accs)
{
    Payee_Id id;
    QString name;
    bool obsolete = false;

    while(reader.readNextStartElement()) {
        if(reader.name().toString() == "Id")
            id = Payee_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "Name")
            name = reader.readElementText();
        else if(reader.name().toString() == "Obsolete")
            obsolete = reader.readElementText() == "true" ? true : false;
        else
            reader.skipCurrentElement();
    }

    return accs.payee_add(CPayee(name, obsolete), id);
}

bool CXMLSerializer::readCategories(QXmlStreamReader& reader, AccountsData& accs)
{
    Category_Id id;
    QString name;
    bool obsolete = false;
    bool income = false;
    Taxcode_Id taxCode_id;
    bool taxcodeSpecific = false;

    while(reader.readNextStartElement()) {
        if(reader.name().toString() == "Id")
            id = Category_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "Name")
            name = reader.readElementText();
        else if(reader.name().toString() == "Obsolete")
            obsolete = reader.readElementText() == "true" ? true : false;
        else if(reader.name().toString() == "Income")
            income = reader.readElementText() == "true" ? true : false;
        else if(reader.name().toString() == "TaxCodeId")
            taxCode_id = Taxcode_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "PropertySpecific")
            taxcodeSpecific = reader.readElementText() == "true" ? true : false;
        else
            reader.skipCurrentElement();
    }

    return accs.category_add(CCategory(name, obsolete, income, taxCode_id, taxcodeSpecific), id);
}

bool CXMLSerializer::readAccounts(QXmlStreamReader& reader, AccountsData& accs)
{
    Account_Id id;
    QString name;
    bool external = false;
    eAccType::AccType type = eAccType::AccType::Credit;
    bool hidden = false;
    eAccLock::AccLock lock = eAccLock::AccLock::Open;
    QDateTime locked_until = QDateTime::currentDateTime();
    QDateTime reconciled_on = QDateTime::currentDateTime();// todo spec for subtract

     while(reader.readNextStartElement()) {
        if(reader.name().toString() == "Id")
            id = Account_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "Name")
            name = reader.readElementText();
        else if(reader.name().toString() == "Ownership")
            external = reader.readElementText() == "true" ? true : false;
        else if(reader.name().toString() == "Type")
        {
            bool ok = false;
            const int value = QMetaEnum::fromType<eAccType::AccType>().keyToValue(reader.readElementText().toStdString().c_str(), &ok);
            if(ok)
                type = static_cast<eAccType::AccType>(value);
        }
        else if(reader.name().toString() == "Hidden")
            hidden = reader.readElementText() == "true" ? true : false;
        else if(reader.name().toString() == "Lock")
        {
            bool ok = false;
            const int value = QMetaEnum::fromType<eAccLock::AccLock>().keyToValue(reader.readElementText().toStdString().c_str(), &ok);
            if(ok)
                lock = static_cast<eAccLock::AccLock>(value);
        }
        else if(reader.name().toString() == "LockedUntil")
            locked_until = QDateTime::fromString(reader.readElementText());
        else if(reader.name().toString() == "ReconciledOn")
            reconciled_on = QDateTime::fromString(reader.readElementText());
        else
            reader.skipCurrentElement();
    }

    return accs.account_add(CAccount(name, external, type, hidden, lock, locked_until, reconciled_on), id);
}

bool CXMLSerializer::readEntries(QXmlStreamReader& reader, AccountsData& accs)
{
        Entry_Id id;
        //eEntryType::EntryType type;
        QString description;
        QString importDescription;
        QDateTime date;
        QUuid reciept_id;
        Payee_Id payee_id;
        Category_Id category_id;
        Property_Id property_id;
        Taxcode_Id taxcode_id;
        Account_Id account_id;
        Account_Id transfer_id;
        double amount;//todo QDecimal

    while(reader.readNextStartElement()) {
        if(reader.name().toString() == "Id")
            id = Entry_Id::fromString(reader.readElementText());//todo
        else if(reader.name().toString() == "Description")
            description = reader.readElementText();
        else if(reader.name().toString() == "ImportDescription")
            importDescription = reader.readElementText();
        else if(reader.name().toString() == "Date")
            date = QDateTime::fromString(reader.readElementText());
        else if(reader.name().toString() == "RecieptNo")
            reciept_id = QUuid::fromString(reader.readElementText());
        else if(reader.name().toString() == "PayeeId")
            payee_id = Payee_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "CatagoryId")
            category_id = Category_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "PropertyId")
            property_id = Property_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "AccountId")
            account_id = Account_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "transferAccountId")
            transfer_id = Account_Id::fromString(reader.readElementText());
        else if(reader.name().toString() == "Amount")
            amount =  666.6; //todo
        else
            reader.skipCurrentElement();
    }

    const CEntry ent = CEntry(
    //, eEntryType::EntryType type
    description
    , importDescription
    , date
    , reciept_id
    , payee_id
    , category_id
    , property_id
    , taxcode_id
    , account_id
    , transfer_id
    , amount);

    return accs.entry_add(CEntry(ent), id);
}

 void CXMLSerializer::importXML(QString path, AccountsData& accs)
{
    QFile file(path);
    if(!file.open(QFile::ReadOnly | QFile::Text)){
        //qDebug() << "Cannot read file" << file.errorString();
        exit(0);
    }

    QXmlStreamReader reader(&file);

    if (reader.readNextStartElement()) {
        if (reader.name().toString() == "Accounts") {
            while(reader.readNextStartElement()){
                if(reader.name().toString() == "TaxCodes") {
                    while(reader.readNextStartElement()){
                        if(reader.name().toString() == "TaxCode")
                            readTaxCode(reader, accs);
                        else if (reader.name().toString() == "Categories")
                            readCategories(reader, accs);
                        else if (reader.name().toString() == "Properties")
                            readProperties(reader, accs);
                        else if (reader.name().toString() == "Payees")
                            readPayees(reader, accs);
                        else if (reader.name().toString() == "Accounts")
                            readAccounts(reader, accs);
                        else if (reader.name().toString() == "Entries")
                            readEntries(reader, accs);
                        else {
                            QString test = reader.name().toString();
                            reader.skipCurrentElement();
                        }
                    }
                }
                else {
                    QString test = reader.name().toString();
                    reader.skipCurrentElement();
                }
            }
        }
        else
            reader.raiseError(QObject::tr("Incorrect file"));
    }

}
