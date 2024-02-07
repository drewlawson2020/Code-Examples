# Generated by Django 4.2.4 on 2023-10-28 19:16

from django.db import migrations, models


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Assignment',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('title', models.CharField(max_length=200)),
                ('description', models.TextField()),
                ('deadline', models.DateField(blank=True)),
                ('weight', models.IntegerField()),
                ('points', models.IntegerField()),
            ],
        ),
        migrations.CreateModel(
            name='Submission',
            fields=[
                ('id', models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('author', models.TextField(blank=True)),
                ('grader', models.TextField(blank=True)),
                ('file', models.FilePathField(path='/')),
                ('score', models.FloatField()),
            ],
        ),
    ]
