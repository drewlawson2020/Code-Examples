# Generated by Django 4.2.4 on 2023-10-30 23:38

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('grades', '0004_alter_submission_file'),
    ]

    operations = [
        migrations.AlterField(
            model_name='submission',
            name='file',
            field=models.FilePathField(path='/submissions/'),
        ),
    ]
