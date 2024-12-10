#ifndef CXMLSERIALIZER_H
#define CXMLSERIALIZER_H

#include <QXmlStreamReader>

#include "accountsdata.h"

class CXMLSerializer
{
public:
    CXMLSerializer();
    static void importXML(QString path, AccountsData& acc);
    static void exportXML(QString path, AccountsData& acc);
private:
    static bool readTaxCode(QXmlStreamReader& reader, AccountsData& acc);
    static bool readProperties(QXmlStreamReader& reader, AccountsData& acc);
    static bool readPayees(QXmlStreamReader& reader, AccountsData& acc);
    static bool readCategories(QXmlStreamReader& reader, AccountsData& acc);
    static bool readAccounts(QXmlStreamReader& reader, AccountsData& acc);
    static bool readEntries(QXmlStreamReader& reader, AccountsData& acc);
};

#endif // CXMLSERIALIZER_H
